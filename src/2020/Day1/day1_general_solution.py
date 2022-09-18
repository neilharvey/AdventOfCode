import sys
from operator import mul
from functools import reduce

# General purpose but slow; could be improved with memoization

def find_entries(entries:list, sum:int, number_of_entries:int) -> list:
  if number_of_entries == 0:
    return []
  if number_of_entries == 1 and sum in entries:
    return [sum]
  
  for item in entries:
    subset = [n for n in entries if n != item]
    result = find_entries(subset, sum - item, number_of_entries - 1)    
    if result:
      result.append(item)
      return result

  return []  
  
def expense_report(entries:list, sum:int, number_of_entries:int) -> int:
  entries_to_sum = find_entries(entries, sum, number_of_entries)
  return reduce(mul, entries_to_sum)

path = sys.argv[1]
with open(path) as f:
   values = list(map(int, f.readlines()))

print(f'Part One: {expense_report(values, 2020, 2)}')
print(f'Part Two: {expense_report(values, 2020, 3)}')