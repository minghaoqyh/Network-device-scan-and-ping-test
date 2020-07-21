# BNS Scanner 
This is the basic network scanner that can scan the all devices which are in the connected network and give their ip address and mac address and os version also. This is built using scapy.
Following the below isntructions.

clone the repository and go into the reposistory folder.

#### Installing dependencies

install python dependencies using requirements,txt with the follwing command.( We have to install only the scapy dependecy)

pip3 install -r requirements.txt

sudo python3 network_scanner.py -t [IP address range]

![Alt text](https://user-images.githubusercontent.com/11618498/67461951-c265e480-f668-11e9-9598-8eb996cf0d25.png)
in the IP address range variable, you want to put your range in the CIDR notation(192.168.1.1/24)

```
Traceback (most recent call last):
  File "network_scanner.py", line 1, in <module>
    import scapy.all as scapy
ModuleNotFoundError: No module named 'scapy'
```

 If you have this problem download scapy from this command
 
 ```
sudo apt-get install python-scapy
```
More details can be found in https://medium.com/@cvaram96/creating-own-network-scanner-using-python-f11a50a5ff77?source=friends_link&sk=64fbe1a530bff1a6ace92d778b232a97


