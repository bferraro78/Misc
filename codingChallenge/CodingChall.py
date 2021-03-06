#### Benjamin Ferraro
# Coding Challange 
import sys
import random
import json
import requests



####################### ALGORITHM
# This function checks rows, columns, and available diagonal sections 
# to determine which best 11 scholarships will mulitply out to the greatest 
# student savings. 
## For each row, col, or diagonal, the product of the inital s(11) scholarships
# are mltiplied and determined immediately. Then, using rolling multiplication,
# to determine the product of the next section of numbers.
# This is done so we do not have to multiply s numbers every time.
# After the inital product of s numbers, we only multply and divide once for each scholarship in that row, col or diag.
def max_scholarshipALG(scholarshipArr):
	maxScholarship = 0 
	retSequence = []

# Diagonal
## All diagonals of x = 0
	tmp = []
	x = 0
	while x <= n-s:
		itr = 0
		productTMP = 1

		# First section of scholarships calculation
		countX = 0
		countY = x
		while countX < s:
			productTMP *= scholarshipArr[countX][countY]
			tmp.insert(0,scholarshipArr[countX][countY]) # record inital scholarship numbers

			countX += 1
			countY += 1
		
		itr += 1

		# Replace largest scholarship
		if productTMP > maxScholarship:
			maxScholarship = productTMP
			retSequence = []
			for t in tmp:
				retSequence.insert(0, t)

		while itr <= (n-s)-x:
			tmp.pop()
			tmp.insert(0, scholarshipArr[itr+s-1][(itr+s-1)+x]) # update tmp list
			productTMP *= (scholarshipArr[itr+s-1][(itr+s-1)+x]) 
			productTMP /= (scholarshipArr[itr-1][(itr-1)+x]) # rolling calcualtion
			itr += 1

			# Replace largest scholarship
			if productTMP > maxScholarship:
				maxScholarship = productTMP
				retSequence = []
				for t in tmp:
					retSequence.insert(0, t)
		tmp = []
		x += 1

## All diagonals the y = 0
	tmp = []
	y = 0
	while y <= n-s:
		itr = 0
		productTMP = 1

		# First section of scholarships calculation
		countX = y
		countY = 0
		while countY < s:
			tmp.insert(0, scholarshipArr[countX][countY]) # record inital scholarship numbers
			productTMP *= scholarshipArr[countX][countY]
			countX += 1
			countY += 1

		itr += 1

		# Replace largest scholarship
		if productTMP > maxScholarship:
			maxScholarship = productTMP
			retSequence = []
			for t in tmp:
				retSequence.insert(0, t)

		while itr <= (n-s)-y:
			tmp.pop()
			tmp.insert(0, scholarshipArr[(itr+s-1)+y][itr+s-1])
			productTMP *= (scholarshipArr[(itr+s-1)+y][itr+s-1]) 
			productTMP /= (scholarshipArr[(itr-1)+y][itr-1]) # rolling calcualtion
			itr += 1
			
			# Replace largest scholarship
			if productTMP > maxScholarship:
				maxScholarship = productTMP
				retSequence = []
				for t in tmp:
					retSequence.insert(0, t)
		tmp = []
		y += 1


## ROWS
	tmp = []
	for x in range(0, n): # for every arr
		itr = 0
		productTMP = 1
		while itr <= n-s:
			if itr == 0: # First section of scholarships calculation
				count = 0
				while count < s:
					tmp.insert(0,scholarshipArr[x][count]) # record inital scholarship numbers
					productTMP *= scholarshipArr[x][count]
					count += 1

				itr += 1
			else:
				tmp.pop()
				tmp.insert(0, scholarshipArr[x][itr+s-1])
				productTMP *= (scholarshipArr[x][itr+s-1]) 
				productTMP /= (scholarshipArr[x][itr-1]) # rolling calcualtion
				itr += 1

			# Replace largest scholarship
			if productTMP > maxScholarship:
				maxScholarship = productTMP
				retSequence = []
				for t in tmp:
					retSequence.insert(0, t)
		tmp = []


## COLUMNS
	tmp = []
	for y in range(0, n): # for every arr
		itr = 0
		productTMP = 1
		while itr <= n-s:
			if itr == 0: # First section of scholarships calculation
				count = 0
				while count < s:
					tmp.insert(0, scholarshipArr[count][y]) # record inital scholarship numbers
					productTMP *= scholarshipArr[count][y]
					count += 1
				itr += 1
			else:
				tmp.pop()
				tmp.insert(0, scholarshipArr[itr+s-1][y])
				productTMP *= (scholarshipArr[itr+s-1][y]) 
				productTMP /= (scholarshipArr[itr-1][y]) # rolling calcualtion
				itr += 1

			# Replace largest scholarship
			if productTMP > maxScholarship:
				maxScholarship = productTMP
				retSequence = []
				for t in tmp:
					retSequence.insert(0, t)
		tmp = []

	## BUILDING JSON OBJECT
	data = {}
	data['total'] = maxScholarship
	data['sequence'] = retSequence
	json_data = json.dumps(data)
	print json_data
	return json_data

#### END ALGORITHM



### REST API
resp = requests.get('/max_scholarship/') # read in NxN matrix
matrix = json.load(resp) 

n = len(matrix['data']) # Gather n
s = 11 # number of scholarships

# NxN must be greater or equal to 100
if n < 100:
	sys.exit()

scholarshipMatrix = max_scholarshipALG(matrix['data']) # Run algorithm on scholarship matrix
requests.post('/max_scholarship/', json=scholarshipMatrix) # Content-Type is auto set to 'application/json


