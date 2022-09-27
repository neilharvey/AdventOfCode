import sys

def dump(matrix):
    for m in matrix:
        print(m)

def get_adjacent(seating):

    cols = len(seating[0])
    rows = len(seating)
    adjacent = list(map(lambda i: [0] * len(i), seating))

    for col in range(0, cols):
        for row in range(0, rows):
            adjacent[row][col] = 0
            for x in range(col - 1, col + 2):
                for y in range(row -1, row + 2):
                    if (x != col or y != row) and x >= 0 and y >= 0 and x < cols and y < rows:
                        if seating[y][x] == '#':
                            adjacent[row][col] += 1    
    return adjacent
    

path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
with open(path) as f:
   seating = list(map(list, f.read().splitlines()))

state_changed = True
i = 0
while state_changed:
    i += 1
    #print(f"== Round {i}: ==")
    state_changed = False
    adjacent_seats = get_adjacent(seating)
    for row in range(0, len(seating)):
        for col in range(0, len(seating[row])):
            if seating[row][col] == 'L' and adjacent_seats[row][col] == 0:
                seating[row][col] = '#'
                state_changed = True
            if seating[row][col] == '#' and adjacent_seats[row][col] >= 4:
                seating[row][col] = 'L'
                state_changed = True    
    #dump(seating)
    #dump(adjacent_seats)

occupied_seats = sum(map(lambda row: sum(map(lambda seat:1 if seat == '#' else 0, row)), seating))
print(occupied_seats)