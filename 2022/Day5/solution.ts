import { readInput } from '../aoc';
const input = readInput();

const stack_count = (input[0].length + 1) / 4;
const move_regex = /.+\*.+/;

console.log(`Stacks: ${stack_count}`);
let stacks: Array<Array<string>> = Array(stack_count);
for(let i=0; i<stack_count; i++) {
    stacks[i] = [];
}

input.forEach(line => {

    // crates
    if (line.indexOf("[") !== -1) {
        for (let i = 0; i < stack_count; i++) {
            var crate = line[1 + (i * 4)];
            if(crate !== ' ') {
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

        for(let i=0; i< quantity; i++) {
            let crate = <string>stacks[from - 1].shift();
            stacks[to - 1].unshift(crate);
        }
    }
});

let answer = "";

stacks.forEach(stack => {
    answer += stack[0];
})

console.log(answer);