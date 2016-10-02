// app.js
// Based on Google's App Engine demos.

// [START app]
'use strict';

var http = require('http');
var express = require('express');
var expressWs = require('express-ws');
var expressWs = expressWs(express());
var request = require('request');

var app = expressWs.app;
app.set('view engine', 'jade');

// Use express-ws to enable web sockets.
require('express-ws')(app);

// We're assuming we only have one mobile device for now.
// More will require checking IDs and disambiguating the data.
var newOrientation = null;
var newAcceleration = null;

// Motion monitoring and republishing service.
app.ws('/serve', function(ws) {
  ws.on('message', function(msg) {
    if (msg.startsWith("o")) {
      newOrientation = msg;
    } else {
      newAcceleration = msg;
    }
  });
});
var wss = expressWs.getWss('/serve');

app.get('/', function(req, res) {
  getExternalIp(function(externalIp) {
    res.render('index.jade', {externalIp: externalIp});
  });
});

// [START external_ip]
// To use websockets on App Engine, it's necessary to connect directly to an
// application instance using the instance's public external IP. This IP is
// obtained here from the metadata server.
var METADATA_NETWORK_INTERFACE_URL = 'http://metadata/computeMetadata/v1/' +
    '/instance/network-interfaces/0/access-configs/0/external-ip';

function getExternalIp (cb) {
  var options = {
    url: METADATA_NETWORK_INTERFACE_URL,
    headers: {
      'Metadata-Flavor': 'Google'
    }
  };

  request(options, function (err, resp, body) {
    if (err || resp.statusCode !== 200) {
      console.log('Error while talking to metadata server, assuming localhost');
      return cb('localhost');
    }
    return cb(body);
  });
}
// [END external_ip]

setInterval(function () {
  if (newOrientation != null) {
    wss.clients.forEach(function(client) {
      client.send(newOrientation + "," + Date.now().toString());
    });
    newOrientation = null;
  }
  if (newAcceleration != null) {
    wss.clients.forEach(function(client) {
      client.send(newAcceleration + "," + Date.now().toString());
    });
    newAcceleration = null;
  }
}, 100); // 10 Hz

// Start the websocket server.
var wsServer = app.listen('65080', function () {
  console.log('Websocket server listening on port %s', wsServer.address().port);
});

// Additionally listen for non-websocket connections on the default App Engine
// port 8080. Using http.createServer will skip express-ws's logic to upgrade
// websocket connections.
var server = http.createServer(app).listen(process.env.PORT || '8080', function () {
  console.log('App listening on port %s', server.address().port);
  console.log('Press Ctrl+C to quit.');
});
// [END app]
