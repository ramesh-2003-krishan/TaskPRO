"use client";

import { useEffect, useState, ChangeEvent, FormEvent } from "react";
import { FileQuestionMark } from "lucide-react";
import { Priority } from "../board/TaskCard";

interface FormData{
    Project : string;
    Task : string;
    Priority : Priority;
    DueDate : Date;
    Assignee : string;
}

type FormErrors = Partial<Record<keyof FormData, string>>;

function validate (data: FormData): FormErrors{
    const err: FormErrors={};
    if(!data.Project.trim()) err.Project = "Required";
    if(!data.Task.trim()) err.Task = "Required";
    if(!data.Priority.trim()) err.Priority ="Required";
    if(!data.DueDate.getDate()) err.DueDate ="Required";
    return err;
}

const inputCls = (error?: string) =>
  `w-full border rounded-xl px-3 py-2.5 text-sm outline-none transition ${
    error
      ? "border-red-400 focus:ring-1 focus:ring-red-400"
      : "border-gray-200 focus:border-teal-500 focus:ring-1 focus:ring-teal-500"
  }`;

const INITIAL: FormData={
    Project:"",
    Task:"",
    Priority:"Low",
    DueDate:new Date(),
    Assignee:""
}

export default function FormData(){
    const[data, setData] = useState<FormData>(INITIAL);
    const[error,setError] = useState<FormErrors>({});
    const[done,setDone] = useState(false);

}

