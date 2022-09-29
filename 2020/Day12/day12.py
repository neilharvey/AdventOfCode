import math
import sys

def part_one(instructions):

    ship_position = [0,0] # First index is E-W, second is N-S
    ship_heading  = [1,0] # Inital heading is East

    for instruction in instructions:
        action = instruction[0]
        value = instruction[1]
        if action == 'N':
            ship_position[1] += value
        if action == 'S':
            ship_position[1] -= value
        if action == 'W':
            ship_position[0] -= value
        if action == 'E':
            ship_position[0] += value
        if action == 'F':
            ship_position[0] += ship_heading[0] * value
            ship_position[1] += ship_heading[1] * value
        if action == 'R':
            r = math.radians(-value)
            ship_heading = [int(ship_heading[0] * math.cos(r)) - int(ship_heading[1] * math.sin(r)), int(ship_heading[0] * math.sin(r)) + int(ship_heading[1] * math.cos(r))]
        if action == 'L':
            r = math.radians(value)
            ship_heading = [int(ship_heading[0] * math.cos(r)) - int(ship_heading[1] * math.sin(r)), int(ship_heading[0] * math.sin(r)) + int(ship_heading[1] * math.cos(r))]

    return abs(ship_position[0]) + abs(ship_position[1])

def part_two(instructions):

    ship_position = [0,0] # First index is E-W, second is N-S
    waypoint      = [10,1]

    for instruction in instructions:
        action = instruction[0]
        value = instruction[1]
        if action == 'N':
            waypoint[1] += value
        if action == 'S':
            waypoint[1] -= value
        if action == 'W':
            waypoint[0] -= value
        if action == 'E':
            waypoint[0] += value
        if action == 'F':
            ship_position[0] += waypoint[0] * value
            ship_position[1] += waypoint[1] * value
        if action == 'R':
            r = math.radians(-value)
            waypoint = [int(waypoint[0] * math.cos(r)) - int(waypoint[1] * math.sin(r)), int(waypoint[0] * math.sin(r)) + int(waypoint[1] * math.cos(r))]
        if action == 'L':
            r = math.radians(value)
            waypoint = [int(waypoint[0] * math.cos(r)) - int(waypoint[1] * math.sin(r)), int(waypoint[0] * math.sin(r)) + int(waypoint[1] * math.cos(r))]
 
    return abs(ship_position[0]) + abs(ship_position[1])


path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
with open(path) as f:
   instructions = [(line[0], int(line[1:])) for line in f.read().splitlines()]

print(f"Part One: {part_one(instructions)}")
print(f"Part Two: {part_two(instructions)}")