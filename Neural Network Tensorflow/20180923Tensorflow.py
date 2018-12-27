import numpy as np
import tensorflow as tf
import matplotlib.pyplot as plt

BATCH=15

a = np.loadtxt('Data.txt')
hang=a.shape[0]
X = np.reshape(a,(hang,12))
b = np.loadtxt('InPut.txt')
Y_ = np.reshape(b,(hang,9))

X = np.delete(X,[3,4,5,8,10,11],axis=1)
print("X:\n",X)
print("Y:\n",Y_)

miu = (X.sum(axis=0))/(X.shape[0])
miu = np.reshape(miu,(1,X.shape[1]))
X=X-miu
fai = (((np.square(X)).sum(axis=0))/(X.shape[0]))
fai = np.reshape(fai,(1,X.shape[1]))
X = X/fai*100

print("X:\n",X)
print("Y:\n",Y_)

def get_weight(shape,regularizer):
    w=tf.Variable(tf.random_normal(shape,seed=1),dtype=tf.float32)
    tf.add_to_collection('losses',tf.contrib.layers.l2_regularizer(regularizer)(w))
    return w

def get_bias(shape):
    b= tf.Variable(tf.constant(0.01,shape=shape))
    return b  

x=tf.placeholder(tf.float32,shape=(None,X.shape[1]))
y_=tf.placeholder(tf.float32,shape=(None,Y_.shape[1]))

w1=get_weight([X.shape[1],7],0.01)
b1=get_bias([w1.shape[1]])
a1=tf.nn.relu(tf.matmul(x,w1)+b1)

w2=get_weight([7,8],0.01)
b2=get_bias([w2.shape[1]])
a2=tf.nn.relu(tf.matmul(a1,w2)+b2)

w3=get_weight([8,8],0.01)
b3=get_bias([w3.shape[1]])
a3=tf.nn.relu(tf.matmul(a2,w3)+b3)

w4=get_weight([8,Y_.shape[1]],0.01)
b4=get_bias([Y_.shape[1]])
y=tf.nn.softmax(tf.matmul(a3,w4)+b4)

loss_mse=tf.reduce_mean(tf.square(y-y_))
loss_total=loss_mse+tf.add_n(tf.get_collection('losses'))

train_step=tf.train.AdamOptimizer(0.002).minimize(loss_mse)

with tf.Session() as sess:
    init_op=tf.global_variables_initializer()
    sess.run(init_op)
    STEPS=100000
    for i in range(STEPS):
        start=(i*BATCH)%hang
        end=start+BATCH
        sess.run(train_step,feed_dict={x:X[start:end],y_:Y_[start:end]})
        if i % 2000==0:
            loss_mse_v=sess.run(loss_mse,feed_dict={x:X,y_:Y_})
            print("After %d steps, loss is: %f"%(i,loss_mse_v))
            #print(sess.run(w1))
    y_pre=sess.run(tf.nn.softmax(tf.matmul(tf.nn.relu(tf.matmul(tf.nn.relu(tf.matmul(tf.nn.relu(tf.matmul(x,w1)+b1),w2)+b2),w3)+b3),w4)+b4),feed_dict={x:X})
    summary_writer = tf.summary.FileWriter("log", sess.graph)
    #y_pre=sess.run(tf.nn.softmax(tf.matmul(tf.nn.softmax(tf.matmul(tf.nn.relu(tf.matmul(tf.nn.relu(tf.matmul(tf.nn.relu(tf.matmul(x,w1)+b1),w2)+b2),w3)+b3),w4)+b4),w5)+b5),feed_dict={x:X})
    
    p = np.zeros((hang,9))
    
    for j in range(0,y_pre.shape[0]):
        line = y_pre[j,:]
        line = np.reshape(line,(1,9))
        _positon = np.argmax(line)
        p[j,:] = [0,0,0,0,0,0,0,0,0]
        p[j,_positon] = 1
    
    print("Accuracy: "  + str(np.sum((p == Y_)/(Y_.shape[0]*Y_.shape[1]))))    
    np.savetxt('InputPredicted.txt',p,fmt='%f',delimiter=' ',newline='\r\n')

def predict():
    
    
'''
def used_predict(X, y, parameters):
    """
    This function is used to predict the results of a  L-layer neural network.
    
    Arguments:
    X -- data set of examples you would like to label
    parameters -- parameters of the trained model
    
    Returns:
    p -- predictions for the given dataset X
    """
    
    m = X.shape[1]
    n = len(parameters) // 2 # number of layers in the neural network
    p = np.zeros((9,m))
    
    # Forward propagation
    probas, caches = L_model_forward(X, parameters, num_iterations=1)


    
    # convert probas to 0/1 predictions
    for j in range(0,probas.shape[1]):
        line = probas[:,j]
        #print(str(line.shape))
        line = np.reshape(line,(1,9))
        _positon = np.argmax(line)
        p[:,j] = [0,0,0,0,0,0,0,0,0]
        p[_positon,j] = 1
                
    
    
    #p = probas
    
    
    
    #print results
    #print ("predictions: " + str(p))
    #print ("true labels: " + str(y))
    print("Accuracy: "  + str(np.sum((p == y)/(m*y.shape[0]))))
        
    return p




#X=np.array([(1,2,5),(8,9,9),(11,54,8),(4,74,9),(9,10,4),(3,4,65),(5,6,9)])
#Y=np.array([(1,0,1),(0,1,0)])
layers_dims=[6,2,7,2,8,9]
Parameters = L_layer_model(X, Y, layers_dims, learning_rate = 0.05, num_iterations = 7500, print_cost=False)


#序列化输出训练好的参数
dictfile=open("Parameters",'wb')
pickle.dump(Parameters,dictfile)
dictfile.close()


#Normalizatin
miu = (X.sum(axis=1))/(X.shape[1])
miu = np.reshape(miu,(6,1))
X=X-miu
fai = (((np.square(X)).sum(axis=1))/(X.shape[1]))
fai = np.reshape(fai,(6,1))
print(str(fai))
X = X/fai*100
print("归一化后的X为：\n"+str(X))
np.savetxt('X.txt',X.T,fmt='%f',delimiter=' ',newline='\r\n')



y = np.reshape(b,(2479,9) ).T
#y=np.array([(0,0,1),(1,0,0)])
a = predict(X, y, L_layer_model(X, Y, layers_dims, learning_rate = 0.005, num_iterations = 7500, print_cost=True))
print("Y_hat.T为：\n"+str(a.T))
print("Y_hat.T的大小为："+str(a.T.shape))



np.savetxt('InputPredicted.txt',a.T,fmt='%f',delimiter=' ',newline='\r\n')'''