import sys
from operator import mul
from functools import reduce

def read_locations(path):
    
    with open(path) as f:
        lines = f.readlines()
    return list(map(lambda line: list(map(lambda x: 0 if x == '.' else 1, line.rstrip())), lines))

def trees_encountered(locations, right, down):

    height = len(locations)
    width = len(locations[0])

    x = y = 0
    trees = 0

    while y < height:
        if locations[y][x] == 1:
            trees +=1 
        x = (x + right) % width
        y = y + down

    return trees

if __name__ == '__main__':

    locations = read_locations(sys.argv[1])
    print(f'Part One: {trees_encountered(locations, 3, 1)}')

    slopes = [[1,1],[3,1],[5,1],[7,1],[1,2]]
    encounters = list(map(lambda slope: trees_encountered(locations, slope[0], slope[1]), slopes))
    result = reduce(mul, encounters)
    print(f'Part Two: {result}')