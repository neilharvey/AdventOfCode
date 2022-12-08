import { readLines } from '../aoc';
const input = readLines();

let pairs_contained = 0;
let pairs_overlapped = 0;

input.forEach(x => {

    const assignments = x.split(",");
    const section1 = assignments[0].split("-").map(x => Number(x));
    const section2 = assignments[1].split("-").map(x => Number(x));

    if((section1[0] >= section2[0] && section1[1] <= section2[1]) || (section2[0] >= section1[0] && section2[1] <= section1[1])) {
        pairs_contained++;
    }

    if(Math.max(section1[0], section2[0]) <= Math.min(section1[1], section2[1])) {
        pairs_overlapped++;
    }
});

console.log(`Part One: ${pairs_contained}`);
console.log(`Part Two: ${pairs_overlapped}`);