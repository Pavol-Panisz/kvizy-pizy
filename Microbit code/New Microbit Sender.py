from microbit import *
import radio
import random

#--------INTERNAL-VARIABLES----------------------------------------------------------------------
thisGroup = 0
#-------END-INTERNAL-VARIABLES-------------------------------------------------------------------

#-------------------FUNCTIONS--------------------------------------------------------------------
def config():
    global thisGroup
    thisGroup = random.randint(1, 24)

    while True:


        if button_a.was_pressed():
            thisGroup = thisGroup - 1
        if button_b.was_pressed():
            thisGroup = thisGroup + 1
        #keep the value greater than 0 and lesser than 25
        if thisGroup > 24:
            thisGroup = 1
        if thisGroup < 1:
            thisGroup = 24



        display.show(str(thisGroup))


#---------------END-FUNCTIONS--------------------------------------------------------------------

#----------------LOGIC---------------------------------------------------------------------------

radio.config(group=thisGroup, queue=1)

isInSetup = True
while isInSetup:
    config()

#------------END-LOGIC---------------------------------------------------------------------------