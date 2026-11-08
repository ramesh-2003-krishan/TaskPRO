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

export default mockApi;