import sys

path = sys.argv[1] if len(sys.argv) > 1 else "input.txt"
with open(path) as f:
   input = list(map(int,f.readlines()))

preamble_length = 5 if path == "example.txt" else 25
preamble = set()
index = 0

while index < preamble_length and index < len(input):
    preamble.add(input[index])
    index += 1

is_valid = True
while index < len(input) and is_valid:
    is_valid = False
    for p in preamble:
        if input[index] - p in preamble:
            is_valid = True
            preamble.remove(input[index - preamble_length])
            preamble.add(input[index])
            index += 1

print(index)
invalid_number = input[index]
print(f"Part One: {invalid_number}")

index_low = index - 1
index_high = index

while sum(input[index_low:index_high]) != invalid_number:
    if sum(input[index_low:index_high]) > invalid_number:
        index_high -= 1
        index_low = index_high -1
    else:
        index_low -= 1

range = input[index_low:index_high]
print(f"Part Two: {min(range) + max(range)}")