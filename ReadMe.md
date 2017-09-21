# Welcome to Toy Robot

This toy was developed using C# and .Net Core 2.0. The code was written using TDD and BDD. There are more than 100 tests that validates the logic of this simulation and there are no known defects at the time of shipping.

## Supported Platforms
The app was packaged as a self-hosted application, and should work cross-platform without any need to install any dependency. Out of the box, I have enabled support for the below-mentioned platforms. Other platforms can be supported as well by a small configuration change. The downside to having this app as a self-hosted is the size of the executables is quite large for such a simple application.

#### Windows x64
#### Mac OSX 10.12 x64
#### Ubuntu 16.04-x64 

**Please Node:** The executables were tested on Windows 10, Ubuntu 16.04, and Mac OsX 10.12. [works on my machine :) ].

## Usage
To launch the app, you just need to start the main executable on the desired platform.

On Windows, download the zipped file from the link above, extract it, then start it by running this command:

> `.\Win\Toy.Robot.exe`

On Mac OSX, you can start the app by typing the following command:

> `./Osx/Toy.Robot`

On Ubuntu, you can start the app by typing the following command:

> `./ubuntu/Toy.Robot`

Once the app is started, you will see a list of the supported commands along with the expected parameters' format.

## Dev Dependencies
To extend, build, or run tests, the [.netcore CLI tools](https://www.microsoft.com/net/download/core) is required. No tools are required to run the app.

## Assumptions

* To make it easier to interact with the app, the following assumptions were made:

    * Commands and directions are considered case-insensitive and accepted in upper-case, lower-case and mixed casing. However, the output and the instructions still adhere to the specs and all displayed messages are in UPPER case.
    * The app will take the first valid command on a given line of text. For instance:
    > `move slowly` &nbsp;&nbsp;&nbsp;&nbsp; === parsed as ===> &nbsp;&nbsp;`MOVE`

    * Commands and parameters values are trimmed. For instance:
    > `PLACE 1 , 1 , NORTH`  &nbsp;&nbsp;&nbsp;&nbsp;=== parsed as ===> &nbsp;&nbsp;`PLACE 1,1,NORTH`
    
    > &nbsp;&nbsp;&nbsp;&nbsp; `     leFT`  &nbsp;&nbsp; === parsed as ===> &nbsp;&nbsp;`LEFT`.

* When the user enters invalid commands, the simulation will display error messages. This was not mentioned in the specs, but it's assumed that it is needed.
* If the robot is already placed on the table and given an invalid PLACE command, the robot will stay where it is. This case was not mentioned in the specs.
* The application only has ENGLISH (EN-AU) language/localisaion support.
* The app has an extra command, which is EXIT. This was given to enable the user to STOP/EXIT the simulation gracefully.