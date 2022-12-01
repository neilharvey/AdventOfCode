import { readFileSync } from 'fs';
import path from 'path'

const fileName = path.resolve(__dirname, process.argv[2])
const file = readFileSync(fileName, 'utf-8');
const lines = file.split("\r\n");

let elves: number[] = [];
let calories_held = 0;

lines.forEach((line) => {
    if (line !== "") {
        calories_held += Number(line);
    } 
    else {
        elves.push(calories_held);
        calories_held = 0;
    }
});

elves.push(calories_held);

const sorted_calories = elves.sort((x, y) => y - x);
console.log(`Part One: ${sorted_calories[0]}`);
console.log(`Part Two: ${sorted_calories.slice(0, 3).reduce((a, c) => a + c)}`);