import { readInput } from '../aoc';
const buffer = readInput()[0];

function find_start_of_packet_marker(packet_size:number) {
    for (let n = packet_size; n < buffer.length; n++) {
        let marker = new Set(buffer.slice(n - packet_size, n));
        if (marker.size == packet_size) {
            return n;
        }
    }

    throw new Error("No packet marker found.");
}

console.log(`Part One: ${find_start_of_packet_marker(4)}`);
console.log(`Part Two: ${find_start_of_packet_marker(14)}`);