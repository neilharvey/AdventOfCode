import { readLines } from '../aoc';

const input = readLines();
const instructions: [string, number][] = input.map(line => [line.split(' ')[0], Number(line.split(' ')[1])]);

let headPosition: [number, number] = [0, 0];
let tailPosition: [number, number] = [0, 0];

// Trail is stored as a set of strings because TypeScript tuples do not support value-based equality.
let trail = new Set<string>();
trail.add(tailPosition.toString());

const directions: { [key: string]: [number, number] } = { 'R': [1, 0], 'L': [-1, 0], 'U': [0, 1], 'D': [0, -1] }

instructions.forEach(instruction => {
    while (instruction[1]-- > 0) {
        let direction = directions[instruction[0]];
        headPosition = [headPosition[0] + direction[0], headPosition[1] + direction[1]];

        let diff = [headPosition[0] - tailPosition[0], headPosition[1] - tailPosition[1]];
        let absDiff = [Math.abs(diff[0]), Math.abs(diff[1])];

        if (absDiff[0] > 1 && absDiff[1] == 0) {
            // move l-r
            tailPosition[0] += (diff[0] / absDiff[0]);
        } else if (absDiff[0] == 0 && absDiff[1] > 1) {
            // move u-d
            tailPosition[1] += (diff[1] / absDiff[1]);
        } else if (absDiff[0] > 1 || absDiff[1] > 1) {
            // move diag
            tailPosition[0] += (diff[0] / absDiff[0]);
            tailPosition[1] += (diff[1] / absDiff[1]);
        }
        //console.log(`head -> ${headPosition}; diff = ${diff}; tail -> ${tailPosition}`);
        trail.add(tailPosition.toString());
    }
});

console.log(trail.size);