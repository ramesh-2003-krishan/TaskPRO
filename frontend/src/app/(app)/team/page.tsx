"use client"

import ProfileCard, { ProfileDetail } from "@/components/team/ProfileCard"
import { mockProgress } from "@/lib/mock";
import { useState } from "react";

const initialDetail : ProfileDetail [] = [
    {
        id: 1,
        Name: "Nimal Perera",
        Age: "22",
        Email: "nimal.perera@example.com",
        University: "University of Colombo",
        Profile: "https://i.pravatar.cc/300?img=11",
    },
    {
        id: 2,
        Name: "Kavindi Fernando",
        Age: "21",
        Email: "kavindi.fernando@example.com",
        University: "University of Moratuwa",
        Profile: "https://i.pravatar.cc/300?img=47",
    },
    {
        id: 3,
        Name: "Tharindu Silva",
        Age: "23",
        Email: "tharindu.silva@example.com",
        University: "University of Peradeniya",
        Profile: "https://i.pravatar.cc/300?img=33",
    },
    {
        id: 4,
        Name: "Dilini Jayasuriya",
        Age: "20",
        Email: "dilini.jayasuriya@example.com",
        University: "University of Kelaniya",
        Profile: "https://i.pravatar.cc/300?img=45",
    },
    {
        id: 5,
        Name: "Ruwan Bandara",
        Age: "24",
        Email: "ruwan.bandara@example.com",
        University: "University of Sri Jayewardenepura",
        Profile: "https://i.pravatar.cc/300?img=15",
    },
    {
        id: 6,
        Name: "Sachini Wickramasinghe",
        Age: "22",
        Email: "sachini.w@example.com",
        University: "SLIIT",
        Profile: "",
    },
]

export default function teamPage(){
    const[detail, setDetail] = useState<ProfileDetail[]>(initialDetail);

    const deleteProfile = (id: number) =>
       setDetail((prev) => prev.filter((d) => d.id !== id));

    const editProfile = (id: number) => {
     console.log("edit profile", id); 
     };
    return(
       <section>
           <ProfileCard details={detail} onDelete={deleteProfile} onEdit={editProfile} />
       </section>
    );
}