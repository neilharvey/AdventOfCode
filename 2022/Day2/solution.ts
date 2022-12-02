import { readFileSync } from 'fs';
import path from 'path'

const part1_strategy: { [value: string]:number} = {
    'A X':3 + 1,
    'A Y':6 + 2,
    'A Z':0 + 3,
    'B X':0 + 1,
    'B Y':3 + 2,
    'B Z':6 + 3,
    'C X':6 + 1,
    'C Y':0 + 2,
    'C Z':3 + 3,
}

const part2_strategy: { [value:string]:number} = {
    'A X':0 + 3,
    'A Y':3 + 1,
    'A Z':6 + 2,
    'B X':0 + 1,
    'B Y':3 + 2,
    'B Z':6 + 3,
    'C X':0 + 2,
    'C Y':3 + 3,
    'C Z':6 + 1,
}

function total_score(input:string[], strategy:{ [value:string]:number}) {
    return input.map(x => strategy[x]).reduce((a,c) => a + c);
}

const fileName = path.resolve(__dirname, process.argv[2])
const input = readFileSync(fileName, 'utf-8').split("\r\n");
console.log(`Part One: ${total_score(input, part1_strategy)}`);
console.log(`Part Two: ${total_score(input, part2_strategy)}`);