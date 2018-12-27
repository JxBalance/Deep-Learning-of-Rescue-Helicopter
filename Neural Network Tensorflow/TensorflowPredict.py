import numpy as np
import tensorflow as tf
import matplotlib.pyplot as plt

def predict(s, Parameters):
    m=0
    a=np.loadtxt('InputPredicted.txt')
    b=a[m,:]
    m+=1
    return b
    
    
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



np.savetxt('InputPredicted.txt',a.T,fmt='%f',delimiter=' ',newline='\r\n')
'''