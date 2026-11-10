export const mockApi = {
    get: () => Promise.resolve({ data: {} }),
    post: () => Promise.resolve({ data: {} }),
    put: () => Promise.resolve({ data: {} }),
    delete: () => Promise.resolve({ data: {} }),
};
export const mockProject = [
    {
        id: "1",
        name: "Project 1",
        description: "Description for Project 1"
    },
    {
        id: "2",
        name: "Project 2",
        description: "Description for Project 2"
    }
];
export const mockUser = {
    id: "1",
    name: "John Doe",
    email: "john.doe@example.com",
    plan: "Pro"
};

export const mockWeekly = [
    { day: "Monday", completed: 5 ,Added: 2},
    { day: "Tuesday", completed: 3 ,Added: 1},
    { day: "Wednesday", completed: 7 ,Added: 3},
    { day: "Thursday", completed: 2 ,Added: 0},
    { day: "Friday", completed: 4 ,Added: 2}
];

export const mockProgress = { backlog: 10, inProgress: 5, inReview: 15, done: 3 };

export const ActiveTasks = [
    {
        task: "Design new UI for the dashboard",
        date: "2024-06-01",
        status: "In Progress",
        profile: "/placeholder.svg"
    },
    {
        task: "Implement authentication system",
        date: "2024-06-02",
        status: "In Review",
        profile: "/placeholder.svg"
    },
    {
        task: "Write unit tests for the API",
        date: "2024-06-03",
        status: "Done",
        profile: "/placeholder.svg"
    }
];

export default mockApi;