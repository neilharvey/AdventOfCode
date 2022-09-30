import sys
from functools import reduce

def chinese_remainder(n, a):
   sum = 0
   prod = reduce(lambda a, b: a*b, n)
   for n_i, a_i in zip(n, a):
      p = prod // n_i
      sum += a_i * mul_inv(p, n_i) * p
   return sum % prod
 
def mul_inv(a, b):
   b0 = b
   x0, x1 = 0, 1
   if b == 1: return 1
   while a > 1:
      q = a // b
      a, b = b, a%b
      x0, x1 = x1 - q * x0, x0
   if x1 < 0: x1 += b0
   return x1

path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
with open(path) as f:
   timestamp = int(f.readline())
   input = [bus for bus in f.readline().split(',')]
   bus_ids = [int(id) for id in input if id != 'x']
   bus_offsets = [input.index(str(id)) for id in bus_ids]

arrivals = sorted([(bus, bus - (timestamp % bus)) for bus in bus_ids], key= lambda x: x[1])
next_arrival = arrivals[0]
print(f"Part One: {next_arrival[0] * next_arrival[1]} [bus {next_arrival[0]} in {next_arrival[1]} minutes]")

r = chinese_remainder(bus_ids, bus_offsets)
d = reduce(lambda a, b: a*b, bus_ids)
print(f"Part Two: {d - r} [x = {r} (mod {d})]")