from microbit import *
import radio

#---INIT---

    #DATA CHANGEABLE BY THE USER

#Messages from the PC are processed into one large string.
#The separator separates individual messages in this string
from_pc_separator = ","
baud = 115200 #baudrate
send_to_pc_interval = 20 #the minimal time (in milliseconds) between consecutive prints to the PC

        #MESSAGES WE CAN RECEIVE FROM THE PC
RECV_RECOG_MSG = "RECOG"

        #MESSAGES WE CAN SEND TO THE PC
RECV_ID = "BBCMBIT" #the identification string, which the pc recognizes as this receiver




    #PRIVATE VARIABLES

class States:
    SETUP = 1
    WORK = 2

current_state = States.SETUP
last_call_to_pc_time = 0 #the last time we calld the function send_to_pc (in milliseconds)

    #INITIALIZATION

uart.init(baudrate=baud, bits=8) #so that data can be sent via USB to the PC. Only 8 bits can be sent at a time
display.on()

#---END-INIT---

#---FUNCTIONS---

def process_input_from_pc():

    global current_state

    if uart.any(): #if any data were received from the PC
        msg_bytes = uart.read() #the return value of uart.read() is a bytes object
        msg_str = str(msg_bytes, "ASCII")

        for msg in msg_str.split(from_pc_separator):

            if msg == "": #empty message
                pass

            elif msg == RECV_RECOG_MSG:
                current_state = States.WORK

#sends the msg to the pc, making sure not to send consecutive messages faster than send_to_pc_interval
def send_to_pc(msg):

    global last_call_to_pc_time

    delta_time = running_time() - last_call_to_pc_time #time since last call to this function
    time_called_send_to_pc = running_time()

    if delta_time >= send_to_pc_interval:
       print(msg)
    else:
        sleep(send_to_pc_interval - delta_time)
        print(msg)



#---END-FUNCTIONS---

#---LOGIC---

while True:

    if current_state == States.SETUP:
        send_to_pc(RECV_ID) #send our identification string. The pc uses it to
        process_input_from_pc()
        sleep(send_to_pc_interval) #pointless, but just for good measure

    elif current_state == States.WORK:
        process_input_from_pc()

        #TODO this part from now on seems like something specific. Up until now, this file was a beautiful abstract microbit receiver - a device
        #with two functionalities:  1 - setting up bi-directional communication with the pc
        #                           2 - processing input from the pc and sending messages to the pc
        #wouldn't it be cool, if you made this into a base class from which all future specialized microbit receivers could inherit?

        #you could make the process_input_from_pc function virtual, and anything in the States.WORK loop would be also overrideable.
        #Because if you think about it, States.SETUP is always the same for any kind of microbit receiver. But the States.WORK loop
        #isn't. What COULD be the only thing shared by any receiver is that in the WORK loop, the function process_input_from_pc()
        #could be called.

        #also, make a mechanism so that any message receiver by a microbit sender gets forwarded to the PC. That and nothing more, at least
        #in this base class, that is
        if accelerometer.current_gesture() == "face down":

            send_to_pc("face down")
            sleep(send_to_pc_interval)





