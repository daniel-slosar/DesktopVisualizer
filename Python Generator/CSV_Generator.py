import random
import csv
import time

def gen_random_ids(extended):
    pole_CAN_ids=[]
    for i in range(5):
        pole_CAN_ids.append(gen_Ids(extended))
    
    return pole_CAN_ids

def gen_Ids(extended):

    if(extended):
        id = random.randint(1,536870912)#2 na 29 = 29
    else:
        id = random.randint(1,2048) #2 na 11 = 11bitov
    
    return f'{id:0x}'

idExtended = gen_random_ids(True)
idStandard = gen_random_ids(False)

def random_CAN_ID(isFD):
    if(isFD):
        if(bool(random.getrandbits(1))):
            return idExtended[random.randint(0,len(idExtended)-1)]
        else:
            return idStandard[random.randint(0,len(idStandard)-1)]
    else:
        return idStandard[random.randint(0,len(idStandard)-1)]

#print(gen_Ids(True))    

def CAN_FD_Write(isFD):
    if isFD:
        return 1
    else:
        return 0

def timeNs(rn):
    return int(time.time_ns()/1000000)+rn

def random_data(numBytes):
    numChars = numBytes*2 #pocet znakov v hex
    hex_str = "0123456789abcdef"
    data = ''.join([random.choice(hex_str) for x in range(numChars)])
    return data

#print(random_CAN_ID(isFD))
#print(CAN_FD_Write(isFD))

def random_DLC(numBytes):
    if numBytes <=8:
        dlc = numBytes
    else:
        match numBytes:
            case 12:# bajty
                dlc = 9
            case 16:
                dlc = 10
            case 20:
                dlc = 11
            case 24:
                dlc = 12
            case 32:
                dlc = 13
            case 48:
                dlc = 14
            case 64:
                dlc = 15
            case _:
                raise Exception("Error")
    return dlc

def rrs():
    rrs_bool = bool(random.getrandbits(1))#1
    if rrs_bool:
        return str(1)
    else:
        choices = [0, ""]
        return str(random.choice(choices))

def ide():
    return str(random.randint(0,1))

def fdf():
    return str(random.randint(0,1))

def res():
    return str(random.randint(0,1))

def brs():
    return str(random.randint(0,1))


#print(random_DLC(64))

def main():
    with open('CanInputTest.csv', 'w', newline='') as file:
        FDLengths = [12,16,20,24,32,48,64]
        isFD = bool(random.getrandbits(1))
        rn = random.randint(5000,10000)

        writer = csv.writer(file)
        writer.writerow(["CAN FD;Start us;CAN ID;RRS/RTR;IDE;FDF/r0;BRS;ESI;DLC;DATA"])
        
        for i in range(1000):
            rn1 = random.randint(1000,9000)
            isFD = bool(random.getrandbits(1))
            
            if isFD == True: 
                numBytes = FDLengths[random.randint(0,6)] 
            else: 
                numBytes = (random.randint(1,8))
            
            rn+=rn1
            writer.writerow([str(CAN_FD_Write(isFD))+";"+str(timeNs(rn))+";"+str(random_CAN_ID(isFD))+";"+rrs()+";"+ide()+";"+fdf()+";"+res()+";"+brs()+";"+str(random_DLC(numBytes))+";"+str(random_data(numBytes))])

main()
