# kvizy-pizy
A who wants to be a millionaire game, controlled with microcontrollers

Currently (30.6.2020) It is unfinished.
It consists of a Unity project and code for the bbc:microbit microcontrollers. There's always 1 microcontroller which forwards signals received via radio to the PC using serial communication. The Unity asset which reads the serial port is called Ardity. The voting devices, which are also microbits communicate with the receiver via radio, each in a different radio group. 

Explanation of the 3 directories you'll first encounter:
The kvízy-pízy unity project (with the file IO) is called "Kviz". "BBC Microbit Unity IO" is the unity project which contains the Ardity setup that reads the serial port and logs the messages received. "Microbit code" contains code for the microcontrollers
