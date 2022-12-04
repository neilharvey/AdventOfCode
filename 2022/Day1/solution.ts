import { readInput } from '../aoc';
const lines = readInput();

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