import { readLines } from '../aoc';

function simulateKnots(instructions: [string, number][], length: number) {

    const directions: { [key: string]: [number, number] } = { 'R': [1, 0], 'L': [-1, 0], 'U': [0, 1], 'D': [0, -1] }

    let rope: [number, number][] = [];
    for(let i=0; i<length; i++) rope[i] = [0,0];

    // Trail is stored as a set of strings because TypeScript tuples do not support value-based equality.
    let trail = new Set<string>();
    trail.add([0, 0].toString());

    instructions.forEach(instruction => {
        let steps = instruction[1];
        while (steps-- > 0) {
            let direction = directions[instruction[0]];
            let head = rope[0];
            rope[0] = [head[0] + direction[0], head[1] + direction[1]];
            for (let i = 0; i < rope.length - 1; i++) {
                moveKnot(rope[i], rope[i + 1]);
            }
            let tail = rope[length - 1]
            trail.add(tail.toString());
        }
    });

    return trail;
}

function moveKnot(headPosition: [number, number], tailPosition: [number, number]) {

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
}


const input = readLines();
const instructions: [string, number][] = input.map(line => [line.split(' ')[0], Number(line.split(' ')[1])]);
console.log(`Part One: ${simulateKnots(instructions, 2).size}`);
console.log(`Part Two: ${simulateKnots(instructions, 10).size}`);