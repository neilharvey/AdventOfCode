import sys

def get_nth_number(numbers, target):
    spoken_numbers = {}
    for (turn,n) in enumerate(numbers):
        spoken_numbers[n] = [turn]

    turn = len(numbers)
    last = numbers[turn - 1]

    while turn < target:

        if len(spoken_numbers[last]) == 1:
            last = 0
        else:
            last_spoken = spoken_numbers[last][-2:]
            last  = last_spoken[1] - last_spoken[0]

        if last in spoken_numbers.keys():
            spoken_numbers[last].append(turn)
        else:
            spoken_numbers[last] = [turn]

        turn += 1    
    
    #print(spoken_numbers)
    return last

path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
with open(path) as f:
    input = list(map(int, f.read().split(',')))

print(f"Part One: {get_nth_number(input, 2020)}")
print(f"Part Two: {get_nth_number(input, 30000000)}")