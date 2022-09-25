import sys

def run_boot_code(instructions):
    visited = [False] * len(instructions)

    pc = 0
    acc = 0

    while pc < len(instructions) and not visited[pc]:
        visited[pc] = True
        op = instructions[pc]
        if op[0] == "acc":
            acc += op[1]
            pc += 1
        if op[0] == "jmp":
            pc += op[1]
        if op[0] == "nop":
            pc += 1
    
    if pc == len(instructions):
        return (True, acc)
    else:
        return (False, acc)

def repair_corrupt_instruction(instructions):
    pc = 0
    while pc < len(instructions):
        opcode = instructions[pc]
        if opcode[0] == "jmp":
            instructions[pc] = ("nop", opcode[1])
        elif opcode[0] == "nop":
            instructions[pc] = ("jmp", opcode[1])         
        result = run_boot_code(instructions)
        if result[0]:
            return result[1]
        else:
            instructions[pc] = opcode
        pc += 1
    return 0            

with open(sys.argv[1]) as f:
   input = f.readlines()

instructions = [(p[0], int(p[1])) for p in map(lambda line: line.split(), input)]
acc_infinite_loop = run_boot_code(instructions)[1]
print(f"Part One: {acc_infinite_loop}")

acc_repaired = repair_corrupt_instruction(instructions)
print(f"Part Two: {acc_repaired}")
