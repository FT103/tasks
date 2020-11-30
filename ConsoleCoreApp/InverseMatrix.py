import numpy as np
import sys

# "3 & 31 \\ 7 & 24"

matrix = [list(map(int, i.split(' & '))) for i in ' '.join(sys.argv[1:]).split(' \\\\ ')]
inverted_matrix = np.linalg.inv(matrix)
answer = [' & '.join(map(str, line)) for line in inverted_matrix]
print(' \\\\ '.join(answer))

