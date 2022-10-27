import sys

def crab_cups(cups, start, total_moves):

    move = 1
    min_cup = min(cups)
    max_cup = max(cups)
    current = start

    while move <= total_moves:

        # The crab picks up the three cups that are immediately clockwise of the current cup. 
        x = cups[current - 1]
        y = cups[x - 1]
        z = cups[y - 1]
        pick_up = [x, y, z]
        
        # They are removed from the circle; cup spacing is adjusted as necessary to maintain the circle.
        cups[current - 1] = cups[z - 1]

        # The crab selects a destination cup: the cup with a label equal to the current cup's label minus one.     
        destination = current - 1 if current - 1 >= min_cup else max_cup

        # If this would select one of the cups that was just picked up, 
        # the crab will keep subtracting one until it finds a cup that wasn't just picked up. 
        # If at any point in this process the value goes below the lowest value on any cup's label, 
        # it wraps around to the highest value on any cup's label instead.
        while destination in pick_up:
            destination = destination - 1 if destination - 1 >= min_cup else max_cup 

        # The crab places the cups it just picked up so that they are immediately clockwise of the destination cup. 
        # They keep the same order as when they were picked up.
        tmp = cups[destination - 1]
        cups[destination - 1] = x
        cups[x - 1] = y
        cups[y - 1] = z
        cups[z - 1] = tmp

        # The crab selects a new current cup: the cup which is immediately clockwise of the current cup.
        current = cups[current - 1]

        move += 1

    return cups

def get_clockwise_from_one(cups):

    current = cups[cups.index(1)]
    result = []
    for _ in range(0, len(cups) - 1):
        result.append(cups[current - 1])
        current = cups[current - 1]

    return result

def get_cups(input):

    # Since the cups are a fixed length we can represent the circular linked list of cups using an array,
    # i.e. for a given cup n, a[n - 1] = m where m is the next cup clockwise.
    # In retrospect this was a shit idea and I should have just used a linked list.
    size = len(input)
    cups = [0] * size
    for (i, cup) in enumerate(input):
        next = input[(i + 1) % size]
        cups[cup - 1] = next

    return cups

def main():

    path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
    with open(path) as f:
        input = [int(x) for x in f.read()]

    cups = get_cups(input)
    start = input[0]
    part_one_cups = crab_cups(cups[:], start, 100)    
    print(f"Part One: {''.join(map(str, get_clockwise_from_one(part_one_cups)))}")

    extra_cups = range(1 + len(cups), 1 + 1000000)
    input.extend(extra_cups)
    cups = get_cups(input)
    part_two_cups = crab_cups(cups, start, 10000000)
    part_two_cups_clockwise_from_one = get_clockwise_from_one(part_two_cups)
    print(f"Part Two: {part_two_cups_clockwise_from_one[0] * part_two_cups_clockwise_from_one[1]} ({part_two_cups_clockwise_from_one[0]} x {part_two_cups_clockwise_from_one[1]})")

if __name__ == "__main__":
    main()