import sys

def part_one(values):
  for x in values:
    for y in values:
      if x + y == 2020:
        return x * y

def part_two(values):
  for x in values:
    for y in values:
      for z in values:        
        if x + y + z == 2020:
          return x * y * z

path = sys.argv[1]
with open(path) as f:
   values = list(map(int, f.readlines()))

print(f'Part One: {part_one(values)}')
print(f'Part Two: {part_two(values)}')