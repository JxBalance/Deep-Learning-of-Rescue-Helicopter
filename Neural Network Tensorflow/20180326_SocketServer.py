import numpy as np
import pickle
import socket
import threading
from TensorflowPredict import predict



#读入训练好的参数
dictfile=open("Parameters",'rb')
Parameters=pickle.load(dictfile)

# 当新的客户端连入时会调用这个方法
def on_new_connection(client_executor, addr):
    print('Accept new connection from %s:%s...' % addr)
    
    # 发送一个欢迎信息

    # 进入死循环，读取客户端发送的信息。
    while True:
        msg = client_executor.recv(1024).decode('utf-8')
        t = msg.split('|')
        a = np.array(t)
        s = []
        for i in a:
            i = float(i)
            s.append(i)
        s = np.reshape(s,(6,1))
        print(s)
        Y = predict(s, Parameters)
        print("In this frame, the output is: "+str((Y)))
        '''
        client_executor.send((str(Y)).encode('utf-8'))
    client_executor.close()
    print('Connection from %s:%s closed.' % addr)'''



# 构建Socket实例、设置端口号和监听队列大小
listener = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
listener.bind(('192.168.217.1', 12323))
listener.listen(5)
print('Successfully start the server, waiting for connecting!')



# 进入死循环，等待新的客户端连入。一旦有客户端连入，就分配一个线程去做专门处理。然后自己继续等待。
while True:
    client_executor, addr = listener.accept()
    t = threading.Thread(target=on_new_connection, args=(client_executor, addr))
    t.start()
    print(addr)
