import sys
from collections import defaultdict

def read_group_answers(input):
    group_answers = []
    current_group = []
    for line in input:
        if line:
            current_group.append(line)
        else:
            group_answers.append(current_group)
            current_group = []
    group_answers.append(current_group)
    return group_answers

def get_any_question_yes(group_answers):
    total = 0
    for group in group_answers:
        questions_answered = defaultdict(int)
        for person in group:
            for question in person:
                questions_answered[question] += 1
        total += len(questions_answered)                 
    return total

def get_every_question_yes(group_answers):
    total = 0
    for group in group_answers:
        group_iter = iter(group)
        first_person_answers = next(group_iter)
        questions_answered = set(first_person_answers)
        for person_answers in group_iter:
            questions_answered = questions_answered.intersection(set(person_answers))
        total += len(questions_answered)
    return total

path = sys.argv[1]
with open(path) as f:
   input = f.read().splitlines()

group_answers = read_group_answers(input)

print(f"Part One: {get_any_question_yes(group_answers)}")
print(f"Part Two: {get_every_question_yes(group_answers)}")