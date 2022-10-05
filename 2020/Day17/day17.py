import sys

def read_initial_state(path, dimensions):
    with open(path) as f:
        input = f.read().splitlines()

    active_cubes = set()
    
    for (y,line) in enumerate(input):
        for (x, cube) in enumerate(line):
            if cube == '#':
                if dimensions == 4:
                    active_cubes.add((x,y,0,0))
                else:
                    active_cubes.add((x,y,0))

    return active_cubes

def get_neighbours(cube:tuple) -> list:

    dimensions = len(cube)
    if dimensions == 3:
        local_area = [(dx,dy,dz) for dx in range(-1,2) for dy in range (-1,2) for dz in range(-1,2) if (dx != 0 or dy != 0 or dz != 0)]
        neighbours = [(cube[0] + dx, cube[1] + dy, cube[2] + dz) for (dx,dy,dz) in local_area]
    else:
        local_area = [(dx,dy,dz,dw) for dx in range(-1,2) for dy in range (-1,2) for dz in range(-1,2) for dw in range(-1,2) if (dx != 0 or dy != 0 or dz != 0 or dw != 0)]
        neighbours = [(cube[0] + dx, cube[1] + dy, cube[2] + dz, cube[3] + dw) for (dx,dy,dz,dw) in local_area]        

    return neighbours

def get_active_neighbours(active_cubes, cube):

     neighbours = get_neighbours(cube)
     return [n for n in neighbours if n in active_cubes]

def simulate_cycles(active_cubes:set, cycles:int) -> set:

    for _ in range(1, cycles + 1):

        deactivated_cubes = set()
        for cube in active_cubes:
            active_neighbours = len(get_active_neighbours(active_cubes, cube))            
            if not (active_neighbours == 2 or active_neighbours == 3):
                deactivated_cubes.add(cube)

        activated_cubes = set()
        all_neighbours = set([neighbour for cube in active_cubes for neighbour in get_neighbours(cube)])
        for cube in all_neighbours:
            active_neighbours = len(get_active_neighbours(active_cubes, cube))
            if active_neighbours == 3:
                activated_cubes.add(cube)

        active_cubes = active_cubes.difference(deactivated_cubes).union(activated_cubes)

    return active_cubes

path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
pocket_dimension_3d = read_initial_state(path, 3)
pocket_dimension_3d = simulate_cycles(pocket_dimension_3d, 6)
print(f"Part One: {len(pocket_dimension_3d)}")
pocket_dimension_4d = read_initial_state(path, 4)
pocket_dimension_4d = simulate_cycles(pocket_dimension_4d, 6)
print(f"Part Two: {len(pocket_dimension_4d)}")