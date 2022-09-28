import copy
import sys

directions = [(row,col) for row in range(-1, 2) for col in range(-1,2) if row != 0 or col != 0]

def get_seats(seating):
    seats = []
    for row in range(0, len(seating)):
        for col in range(0, len(seating[0])):
            if seating[row][col] != '.':
                seats.append((row,col))
    return seats

def get_adjacent_seats(seats, seating_plan):

    adjacent = [[0] * len(row) for row in seating_plan]

    for seat in seats:
        for direction in directions:
            row = seat[0] + direction[0]
            col = seat[1] + direction[1]
            if row >= 0 and col >= 0 and row < len(seating_plan) and col < len(seating_plan[0]) and seating_plan[row][col] == '#':
                adjacent[seat[0]][seat[1]] += 1

    return adjacent

def get_visible_seats(seats,seating_plan):

    visible = [[0] * len(row) for row in seating_plan]

    for seat in seats:
        for direction in directions:
            scale = 1
            row = seat[0] + (scale * direction[0])
            col = seat[1] + (scale * direction[1])

            while row >= 0 and col >= 0 and row < len(seating_plan) and col < len(seating_plan[0]):

                if seating_plan[row][col] == '.':
                    scale += 1
                    row = seat[0] + (scale * direction[0])
                    col = seat[1] + (scale * direction[1])
                else:
                    if seating_plan[row][col] == '#':
                        visible[seat[0]][seat[1]] += 1
                    break

    return visible

def get_occupied_seats(seating_plan, adacency_func, max_neighbours):

    # We need to take a copy because we will mutate the plan
    plan = copy.deepcopy(seating_plan)
    seats = get_seats(plan)
    state_changed = True

    while state_changed:

        state_changed = False
        adjacent_seats = adacency_func(seats, plan)

        for seat in seats:
            row = seat[0]
            col = seat[1]
            if plan[row][col] == 'L' and adjacent_seats[row][col] == 0:
                plan[row][col] = '#'
                state_changed = True
            elif plan[row][col] == '#' and adjacent_seats[row][col] >= max_neighbours:
                plan[row][col] = 'L'
                state_changed = True              

    return sum(map(lambda row: sum(map(lambda seat:1 if seat == '#' else 0, row)), plan))
    

path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
with open(path) as f:
   seating_plan = list(map(list, f.read().splitlines()))

print(f"Part One: {get_occupied_seats(seating_plan, get_adjacent_seats, 4)}")
print(f"Part Two: {get_occupied_seats(seating_plan, get_visible_seats, 5)}")