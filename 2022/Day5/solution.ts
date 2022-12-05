import { readInput } from '../aoc';
const input = readInput();
const stack_count = (input[0].length + 1) / 4;

function supply_stacks(input: string[], move_multiple: boolean) {

    let stacks: Array<Array<string>> = Array(stack_count);
    for (let i = 0; i < stack_count; i++) {
        stacks[i] = [];
    }

    input.forEach(line => {

        // crates
        if (line.indexOf("[") !== -1) {
            for (let i = 0; i < stack_count; i++) {
                var crate = line[1 + (i * 4)];
                if (crate !== ' ') {
                    stacks[i].push(crate);
                }
            }
        }

        // instructions
        if (line.startsWith("move")) {
            const parts = line.split(" ");
            const quantity = Number(parts[1]);
            const from = Number(parts[3]);
            const to = Number(parts[5]);

            let crates_to_move: string[] = [];
            for (let i = 0; i < quantity; i++) {
                let crate = <string>stacks[from - 1].shift();
                crates_to_move.unshift(crate);
            }

            for (let i = 0; i < quantity; i++) {
                let crate = move_multiple ? crates_to_move.shift() : crates_to_move.pop();
                stacks[to - 1].unshift(<string>crate);
            }
        }
    });

    return stacks.reduce<string>((a, c) => a + c[0], "");
}

console.log(`Part One: ${supply_stacks(input, false)}`);
console.log(`Part Two: ${supply_stacks(input, true)}`);