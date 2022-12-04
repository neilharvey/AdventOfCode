import { readInput } from '../aoc';
const input = readInput();

let pairs_contained = 0;
let pairs_overlapped = 0;

function contains(section1:number[], section2:number[]) {
    return section1[0] >= section2[0] && section1[1] <= section2[1];
}

function overlap(section1:number[], section2:number[]) {
    if(Math.max(section1[0], section2[0]) <= Math.min(section1[1], section2[1])) {
        return true;
    } else {
        return false;
    }
}

input.forEach(x => {

    const assignments = x.split(",");
    const section1 = assignments[0].split("-").map(x => Number(x));
    const section2 = assignments[1].split("-").map(x => Number(x));

    if(contains(section1, section2) || contains(section2, section1)) {
        pairs_contained++;
    }

    if(overlap(section1, section2)) {
        pairs_overlapped++;
    }
});

console.log(`Part One: ${pairs_contained}`);
console.log(`Part Two: ${pairs_overlapped}`);