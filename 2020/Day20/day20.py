import sys
import math

path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
with open(path) as f:
   input = f.read().splitlines()

tile_size = 10
sides = {}

i = 0
while i < len(input):
    tile_id = int(input[i][5:9])

    top = input[i+1]
    bottom = input[i + tile_size]
    left = ''.join([input[k][0] for k in range(i + 1, i + tile_size + 1)])
    right = ''.join([input[k][tile_size - 1] for k in range(i + 1, i + tile_size + 1)])

    tile_sides = [top, bottom, left, right, top[::-1], bottom[::-1], left[::-1], right[::-1]]

    for side in tile_sides:
        if side in sides:
            sides[side].append(tile_id)
        else:
            sides[side] = [tile_id]

    i += tile_size + 2

unique_sides = {}
for item in sides.items():
    if len(item[1]) == 1:
        tile_id = item[1][0]
        if not tile_id in unique_sides:
            unique_sides[tile_id] = 0
        unique_sides[tile_id] += 1

corner_pieces = [tile_id for tile_id in unique_sides if unique_sides[tile_id] == 4]
side_pieces = [tile_id for tile_id in unique_sides if unique_sides[tile_id] == 2]
print(corner_pieces)

print(f"Part One: {math.prod(corner_pieces)}")