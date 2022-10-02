import sys
import math

path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
with open(path) as f:
    program = f.read().splitlines()

mask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
p1_mem = {}
p2_mem = {}

for command in program:
    if 'mask' in command:
        mask = command[7:]
    else:
        # Unmodified values
        address = int(command[command.index("[") + 1:command.index("]")])
        value = int(command[command.index("=") + 2:]) 

        # Part One
        masked_value = value
        for (i, bit) in enumerate(mask):
            bit_index = 36 - i - 1
            if bit == '1':
                masked_value |= (1 << bit_index)
            elif bit == '0':
                masked_value &= ~(1 << bit_index)
        p1_mem[address] = masked_value

        # Part Two
        masked_address = address
        for (i, bit) in enumerate(mask):
            bit_index = 36 - i - 1
            if bit == '1':        
                masked_address |= (1 << bit_index)

        masked_addresses = set()
        masked_addresses.add(masked_address)
        bit_range = range(35 - int(math.log2(address)), 36)

        for i in bit_range:
            bit_index = 36 - i - 1
            if mask[i] == 'X':
                new_masked_addresses = set()
                for masked_address in masked_addresses:
                    new_masked_addresses.add(masked_address | (1 << bit_index))
                    new_masked_addresses.add(masked_address & ~(1 << bit_index))
                masked_addresses = new_masked_addresses

        for masked_address in masked_addresses:
            p2_mem[masked_address] = value

print(f"Part One: {sum(p1_mem.values())}")
print(f"Part Two: {sum(p2_mem.values())}")