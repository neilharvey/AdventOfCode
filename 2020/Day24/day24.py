import sys
import operator

directions = ['e', 'se', 'sw', 'w', 'nw', 'ne']
coordinates = [(1,0,-1),(0,1,-1),(-1,1,0),(-1,0,1),(0,-1,1),(1,-1,0)] 

def read_tokens(line):
    tokens = []
    i = 0
    while i < len(line):
        if line[i] == 'e' or line[i] == 'w':
            tokens.append(line[i])
            i += 1
        else:
            tokens.append(line[i:i+2])
            i += 2
    return tokens


def main():

    path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
    with open(path) as f:
        input = f.read().splitlines()
    
    instructions = map(read_tokens, input)

    # A tile is represented as a (q,r,s) coordinate
    flipped_tiles = set()
    center = (0,0,0)
    for instruction in instructions:
        tile = center
        for direction in instruction:
            index = directions.index(direction)
            move = coordinates[index]
            neighbour = tuple(map(operator.add, tile, move))
            tile = neighbour

        if tile in flipped_tiles:
            flipped_tiles.remove(tile)
        else:
            flipped_tiles.add(tile)

    print(f"Part One: {len(flipped_tiles)}")

if __name__ == "__main__":
    main()