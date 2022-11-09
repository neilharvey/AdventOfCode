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

def get_flipped_tiles(instructions):

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

    return flipped_tiles

def get_neighbours(tile):
    return [tuple(map(operator.add, tile, direction)) for direction in coordinates]

def flip_tiles(flipped_tiles):

    # flipped neighbours for each tile
    black_tiles = {}
    white_tiles = {} 
    
    # init the back tiles first
    for tile in flipped_tiles:
        black_tiles[tile] = 0

    # find all the tiles we need to track
    for tile in flipped_tiles:
         black_tiles[tile] = 0
         neighbours = get_neighbours(tile)
         for neighbour in neighbours:
            if neighbour in flipped_tiles:
                black_tiles[tile] += 1
            else:
                if not neighbour in white_tiles:
                    # It's a tile we haven't seen before
                    white_tiles[neighbour] = 1
                else:
                    # It's one we have seen before
                    white_tiles[neighbour] += 1

    # Any black tile with zero or more than 2 black tiles (i.e. not 1 tile) immediately adjacent to it is flipped to white.
    black_tiles_unflipped = [tile for (tile, count) in black_tiles.items() if count == 1 or count == 2]

    # Any white tile with exactly 2 black tiles immediately adjacent to it is flipped to black.
    white_tiles_flipped = [tile for (tile, count) in white_tiles.items() if count == 2]

    return black_tiles_unflipped + white_tiles_flipped

def main():

    path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
    with open(path) as f:
        input = f.read().splitlines()

    instructions = map(read_tokens, input)
    flipped_tiles = get_flipped_tiles(instructions)
    print(f"Part One: {len(flipped_tiles)}")

    for i in range (1,101):
        flipped_tiles = flip_tiles(flipped_tiles)
    print(f"Part Two: {len(flipped_tiles)}")

if __name__ == "__main__":
    main()