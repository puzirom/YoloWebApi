# YoloWebApi
Some examples how to work with .NET CORE Web Api in a Docker container

Howto build and run soltion: 
1. Clone repo to some local directory. For example C:\Test\ then work folder be C:\Test\YoloWebApi.
2. Open YoloWebApi solution under Visual Studio and build it.
3. Run YoloWebApi project under Docker - it is a default debug project setting.
4. New browser instance will be run and open https://localhost:55477/swagger/index.html 

How to test.
In Swagger UI open each web methot and try to execute it. All methods are implemented as HTTP GET. 
Some method requires input string values. 

1. ReverseString. Returns reversed (inverted) text. 
   You can provide any string value into input field "text". If this field be empty then default text value will be used.

2. ParallWork. Runs 1000 processing tasks with delay 0.1 second and return result time of processing.

3. HashCompute. Compute hash value for provided file. 
   Take files from https://speed.hetzner.de/ (or take any own files) and copy them to work folder (C:\Test\YoloWebApi).
   In input field "filename" should be provided a name of copied file which you would like to test (e.g. 100MB.bin, 1GB.bin, 10GB.bin).

4. AssetMarket. Returns list of Assets and their market price for TOP 100 assets. 

In case if AssetMarket returns error like "SocketException: No such host is known" 
then add into docker daemon.json (C:\ProgramData\Docker\config) following setting:
{
    "dns": ["8.8.8.8"]
}
Details about that docker specification is provided there:
https://docs.docker.com/config/containers/container-networking/
https://dockerlabs.collabnix.com/intermediate/networking/Configuring_DNS.html
