import sys

def read_boarding_passes(path):
    with open(path) as f:
        return f.read().splitlines() 

def get_seat_number(boarding_pass):
    bin = boarding_pass.translate({ord('B'):'1', ord('F'):'0', ord('R'):'1', ord('L'):'0'})
    row = int(bin[0:7], 2)
    col = int(bin[-3:], 2)
    return (row, col)

def get_seat_id(seat_number):
    return (8 * seat_number[0]) + seat_number[1]

def find_empty_seat(seat_ids):
    start = seat_ids[0]
    end = seat_ids[-1]
    all_seats = set(range(start, end + 1))
    return all_seats.difference(seat_ids).pop()

boarding_passes = read_boarding_passes(sys.argv[1])
seat_numbers = map(get_seat_number, boarding_passes)
seat_ids = sorted(map(get_seat_id, seat_numbers))

print(f"Part One: {max(seat_ids)}")
print(f"Part Two: {find_empty_seat(seat_ids)}")