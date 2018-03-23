# Oem Access Test Server
A sample server for testing the OEM Advance Controller

This application is a sample server for the Grosvenor OEM Advance controller to communicate with, full protocol documentation is available from Grosvenor Technology as part of the OEM-Access SDK.

To help you get started you can use the PostMan library we have available https://documenter.getpostman.com/view/326317/RVtvpY6k#c9f33d1b-7496-0a40-f22a-2e73c748737e

There are two parts to the library, a Control API and the actual OEM protocol.  The control API will allow you to interact with the controller via this test server, registering controllers, send updates to them etc.

For the quickest getting started experience, we suggest you use the Virtual OEM Controller application shipped as part of the SDK rather than using physical hardware for your first experiments.

Getting started
===

1) Clone this repo, open the OemAccessTestServer.sln, nuget restore, build and run.
2) Open the Virtual controller application and change the uri in the boot.config to localhost:8080 and click start
3) At this point you should see red 'Auth Failure' messages 
3) Open the postman library, and in the 'Test Server Control API' folder select 'Create Device' and click send.
4) You should now see green status messages, and you can experiment further.
