import sys
import numpy as np

array = np.array(sys.argv[1:])
answer = np.roots(array)
for root in answer:
    print(str(root).replace(' ', '').replace('(', ' ').replace(')', ' '), end=' ')