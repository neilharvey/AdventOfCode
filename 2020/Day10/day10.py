from itertools import permutations
import sys

path = sys.argv[1] if len(sys.argv) > 1 else "input.txt"
with open(path) as f:
   adapters = sorted(map(int,f.readlines()))

differences = {1:1, 3:0}
permutations = {0:1}

for a in adapters:    

    # The data is such that a joltage jump can only ever be a 1 or a 3
    if a - 1 in adapters:
        differences[1] += 1
    else:
        differences[3] += 1

    # The total permutations can be calculated as the sum of routes to a given number 
    permutations[a] = sum(map(lambda i: permutations.get(a - i, 0), range(1,4)))

print (f"Part One: {differences[1] * differences[3]}")
print (f"Part Two: {permutations[max(adapters)]}")