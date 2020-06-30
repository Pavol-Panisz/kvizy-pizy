from microbit import *
import radio

#--------USER-DEFINED----------------------------------------------------------------------------
idMessageToUnity = 'M:B_RECV' #the message Unity recognizes as a receiver
writeToPcDelay = 20 #how long to sleep for after sendind a message to Unity. No sleep = Unity would be unable to keep up
senderListeningDuration = 100 #for how long we listen to each radio sender
splitChar = '#'
setValueChar = '='
str_msg_received = "OK"
#--------END-USER-DEF----------------------------------------------------------------------------

#--------INTERNAL-VARIABLES----------------------------------------------------------------------
isCommunicatingWithUnity = False
groups = [0]
#-------END-INTERNAL-VARIABLES-------------------------------------------------------------------

'''
SOME THINGS TO KEEP IN MIND:
The microbit can only receive messages 8 bytes long, whereas Unity
accepts any length.
'''

#-------------------FUNCTIONS--------------------------------------------------------------------

def sendToUnity(msg):
    print(str(msg))
    sleep(writeToPcDelay)

def processMessages(str_buf, group=None):
    msgs = str_buf.split(splitChar) #splits the string in the buffer into commands separated by splitChar

    for msg in msgs:
        sendToUnity(msg)

        #first, check if the message received is a 'setter' - for ex. 'SD=40'
        str_val = 0
        if msg.find(setValueChar) != -1: #if the message contains the setValueChar character

            splitMsg = msg.split(setValueChar)

            msg = splitMsg[0] #the command itself is always on the left hand side of the splitChar
            str_val = splitMsg[1] #the value is always on the right hand side

        #CHECK FOR RECOGNIZED MESSAGES

        if msg == "":
            #ignore this message - it occurs when splitting for ex "SD=10#".
            #The result of that splitting will be "SD=10" and ""
            pass

        #set the writeToPcDelay
        elif msg == "SD":
            writeToPcDelay = int(str_val)
            str_val = '0'
            sendToUnity("the write to pc delay is now " + str(writeToPcDelay))

        #acknowledge that Unity is now communicating with us
        elif msg == "RECV_OK":
            global isCommunicatingWithUnity
            isCommunicatingWithUnity = True
            sendToUnity("Established succesful bi-directional communication.")

        #add this group to the groups list
        elif msg == "AG":
            sendToUnity("Added " + str_val + " to my list of groups I should listen to.")
            groups.append(int(str_val))

        #for the current group, set that player's value (letter choice)
        elif msg == "SV":
            sendToUnity("set group " + str(group) + " to " + str_val)

            #send back a confirmation message to the sender, so they can stop sending the letter
            radio.send_bytes(bytes(str_msg_received, 'ascii'))

        else:
            sendToUnity("Received an unrecognized message: <" + msg + ">")

def processMessagesFromSenders():
    pass

#---------------END-FUNCTIONS--------------------------------------------------------------------

#----------------LOGIC---------------------------------------------------------------------------

uart.init(baudrate=115200, bits=8)
radio.on()

#LOOP BEFORE STARTED COMMUNICATION WITH UNITY
while not isCommunicatingWithUnity:
    sendToUnity(idMessageToUnity)

    #process messages from Unity ----------------------------------------------------DUPLICATE CODE
    if uart.any():
        b_buf = uart.read() #reads the entire buffer
        str_buf = str(b_buf, 'ascii')

        processMessages(str_buf)


#LOOP AFTER STARTED COMMUNICATION WITH UNITY
while True:

    #process messages from Unity ----------------------------------------------------DUPLICATE CODE
    if uart.any():
        b_buf = uart.read() #reads the entire buffer
        str_buf = str(b_buf, 'ascii')

        processMessages(str_buf)

    #process messages from the senders
    for thisGroup in groups:

        radio.config(group=thisGroup, queue=1)
        sendToUnity("listening to group " + str(thisGroup))
        startTime = running_time()
        timePassed = 0
        while timePassed <= senderListeningDuration:

            incoming = radio.receive_bytes()
            if incoming is not None:
                str_buf = str(incoming, 'ascii')
                processMessages(str_buf, thisGroup)

            timePassed = running_time() - startTime



#------------END-LOGIC---------------------------------------------------------------------------








