"use client";

import { useState } from "react";
import { Bell, Lock, Palette, User } from "lucide-react";


const glass =
  "relative isolate rounded-2xl border border-white/15 bg-white/[0.05] shadow-[0_10px_40px_-10px_rgba(0,0,0,0.6),inset_0_1px_0_rgba(255,255,255,0.18)] backdrop-blur-xl";

const inputCls =
  "w-full rounded-xl border border-white/15 bg-white/[0.06] px-3.5 py-2.5 text-sm text-fg outline-none placeholder:text-muted backdrop-blur-md transition focus:border-accent/60 focus:bg-white/10 focus:ring-1 focus:ring-accent/50";

const primaryBtn =
  "rounded-xl border border-white/20 bg-accent/80 px-5 py-2.5 text-sm font-medium text-white shadow-lg shadow-accent/30 backdrop-blur-md transition-all duration-200 hover:bg-accent active:scale-95";

const tabs = [
  { id: "profile", label: "Profile", icon: User },
  { id: "notifications", label: "Notifications", icon: Bell },
  { id: "appearance", label: "Appearance", icon: Palette },
  { id: "security", label: "Security", icon: Lock },
] as const;

type TabId = (typeof tabs)[number]["id"];


function Field({ label, error, children }: { label: string; error?: string; children: React.ReactNode }) {
  return (
    <label className="block">
      <span className="mb-1.5 block text-xs font-semibold uppercase tracking-wider text-muted">{label}</span>
      {children}
      {error && <span className="mt-1 block text-xs text-danger">{error}</span>}
    </label>
  );
}

function Switch({ on, onChange, label }: { on: boolean; onChange: (v: boolean) => void; label: string }) {
  return (
    <button
      type="button"
      role="switch"
      aria-checked={on}
      aria-label={label}
      onClick={() => onChange(!on)}
      className={`relative h-7 w-12 shrink-0 rounded-full border transition-colors duration-300 ${
        on ? "border-accent/60 bg-accent/80" : "border-white/20 bg-white/10"
      }`}
    >
      <span
        className={`absolute left-0.5 top-0.5 h-5.5 w-5.5 rounded-full bg-white shadow-md transition-transform duration-300 ease-[cubic-bezier(0.34,1.56,0.64,1)] ${
          on ? "translate-x-5" : "translate-x-0"
        }`}
      />
    </button>
  );
}

function ToggleRow({
  title,
  description,
  on,
  onChange,
}: {
  title: string;
  description: string;
  on: boolean;
  onChange: (v: boolean) => void;
}) {
  return (
    <div className="flex items-center justify-between gap-6 rounded-xl border border-white/10 bg-white/[0.04] px-4 py-3.5 transition-colors hover:bg-white/[0.08]">
      <div>
        <div className="text-sm font-medium">{title}</div>
        <div className="mt-0.5 text-xs text-muted">{description}</div>
      </div>
      <Switch on={on} onChange={onChange} label={title} />
    </div>
  );
}

const accents = [
  { name: "Indigo", value: "#6366f1" },
  { name: "Emerald", value: "#22c55e" },
  { name: "Amber", value: "#f59e0b" },
  { name: "Pink", value: "#ec4899" },
  { name: "Cyan", value: "#06b6d4" },
];

export default function SettingsPage() {
  const [tab, setTab] = useState<TabId>("profile");
  const [toast, setToast] = useState("");


  const [profile, setProfile] = useState({
    name: "Alex Park",
    email: "alex.park@example.com",
    role: "Product Designer",
    timezone: "Asia/Colombo",
  });


  const [notify, setNotify] = useState({
    assigned: true,
    comments: true,
    due: true,
    digest: false,
  });


  const [accent, setAccent] = useState("#6366f1");
  const [compact, setCompact] = useState(false);


  const [pw, setPw] = useState({ current: "", next: "", confirm: "" });
  const [pwErrors, setPwErrors] = useState<Partial<Record<keyof typeof pw, string>>>({});
  const [confirmDelete, setConfirmDelete] = useState(false);
  const [deleteText, setDeleteText] = useState("");

  const showToast = (message: string) => {
    setToast(message);
    setTimeout(() => setToast(""), 2200);
  };

 
  const pickAccent = (value: string) => {
    setAccent(value);
    document.documentElement.style.setProperty("--accent", value);
  };

  const savePassword = () => {
    const errors: typeof pwErrors = {};
    if (!pw.current) errors.current = "Required";
    if (pw.next.length < 8) errors.next = "Use at least 8 characters";
    if (pw.confirm !== pw.next) errors.confirm = "Passwords do not match";
    setPwErrors(errors);
    if (Object.keys(errors).length === 0) {
      setPw({ current: "", next: "", confirm: "" });
      showToast("Password updated");
    }
  };

  const initials = profile.name
    .split(" ")
    .map((w) => w[0])
    .join("")
    .slice(0, 2)
    .toUpperCase();

  return (
    <div className="space-y-6">
      <div>
        <h1 className="font-display text-2xl font-bold">Settings</h1>
        <p className="mt-1 text-sm text-muted">Manage your account, notifications and look.</p>
      </div>

      <div className="grid grid-cols-1 gap-6 lg:grid-cols-[240px_minmax(0,1fr)]">
    
        <nav className={`${glass} flex h-fit gap-1 overflow-x-auto p-2 lg:flex-col`}>
          {tabs.map(({ id, label, icon: Icon }) => (
            <button
              key={id}
              onClick={() => setTab(id)}
              className={`flex shrink-0 items-center gap-3 rounded-xl px-4 py-2.5 text-sm transition-all duration-200 ${
                tab === id
                  ? "border border-accent/40 bg-accent/20 font-medium text-fg"
                  : "border border-transparent text-muted hover:bg-white/10 hover:text-fg"
              }`}
            >
              <Icon size={16} />
              {label}
            </button>
          ))}
        </nav>

        
        <section className={`${glass} p-6`}>
          <div className="pointer-events-none absolute inset-0 -z-10 overflow-hidden rounded-2xl">
            <div className="absolute -right-16 -top-16 h-56 w-56 rounded-full bg-accent/35 blur-3xl" />
            <div className="absolute -bottom-20 -left-10 h-56 w-56 rounded-full bg-pink/20 blur-3xl" />
          </div>

       
          {tab === "profile" && (
            <div className="space-y-6">
              <h2 className="font-semibold">Profile</h2>

              <div className="flex items-center gap-4">
                <span className="flex h-16 w-16 items-center justify-center rounded-full border border-white/25 bg-accent font-display text-xl font-bold text-white shadow-lg shadow-accent/30">
                  {initials}
                </span>
                <div>
                  <button className="rounded-lg border border-white/15 bg-white/10 px-3.5 py-1.5 text-sm transition-all hover:bg-white/20 active:scale-95">
                    Change photo
                  </button>
                  <p className="mt-1.5 text-xs text-muted">JPG or PNG, up to 2 MB.</p>
                </div>
              </div>

              <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
                <Field label="Full name">
                  <input
                    className={inputCls}
                    value={profile.name}
                    onChange={(e) => setProfile({ ...profile, name: e.target.value })}
                  />
                </Field>
                <Field label="Email">
                  <input
                    type="email"
                    className={inputCls}
                    value={profile.email}
                    onChange={(e) => setProfile({ ...profile, email: e.target.value })}
                  />
                </Field>
                <Field label="Job title">
                  <input
                    className={inputCls}
                    value={profile.role}
                    onChange={(e) => setProfile({ ...profile, role: e.target.value })}
                  />
                </Field>
                <Field label="Time zone">
                  <select
                    className={inputCls}
                    value={profile.timezone}
                    onChange={(e) => setProfile({ ...profile, timezone: e.target.value })}
                  >
                    {["Asia/Colombo", "Europe/London", "America/New_York", "Asia/Tokyo"].map((z) => (
                      <option key={z} value={z} className="bg-[#14141f]">
                        {z}
                      </option>
                    ))}
                  </select>
                </Field>
              </div>

              <div className="flex justify-end">
                <button className={primaryBtn} onClick={() => showToast("Profile saved")}>
                  Save changes
                </button>
              </div>
            </div>
          )}

        
          {tab === "notifications" && (
            <div className="space-y-4">
              <h2 className="font-semibold">Notifications</h2>
              <ToggleRow
                title="Task assigned to me"
                description="When someone assigns you a task."
                on={notify.assigned}
                onChange={(v) => setNotify({ ...notify, assigned: v })}
              />
              <ToggleRow
                title="Comments"
                description="When someone comments on your tasks."
                on={notify.comments}
                onChange={(v) => setNotify({ ...notify, comments: v })}
              />
              <ToggleRow
                title="Due date reminders"
                description="A reminder the day before a task is due."
                on={notify.due}
                onChange={(v) => setNotify({ ...notify, due: v })}
              />
              <ToggleRow
                title="Weekly digest"
                description="A summary email every Monday."
                on={notify.digest}
                onChange={(v) => setNotify({ ...notify, digest: v })}
              />
              <div className="flex justify-end pt-2">
                <button className={primaryBtn} onClick={() => showToast("Notification settings saved")}>
                  Save changes
                </button>
              </div>
            </div>
          )}

      
          {tab === "appearance" && (
            <div className="space-y-6">
              <h2 className="font-semibold">Appearance</h2>

              <div>
                <div className="mb-3 text-xs font-semibold uppercase tracking-wider text-muted">Accent colour</div>
                <div className="flex flex-wrap gap-3">
                  {accents.map((a) => (
                    <button
                      key={a.value}
                      onClick={() => pickAccent(a.value)}
                      aria-label={a.name}
                      title={a.name}
                      className={`h-10 w-10 rounded-full border-2 transition-all duration-200 hover:scale-110 active:scale-95 ${
                        accent === a.value ? "border-white shadow-lg" : "border-white/20"
                      }`}
                      style={{ backgroundColor: a.value }}
                    />
                  ))}
                </div>
                <p className="mt-2 text-xs text-muted">The whole app changes colour straight away.</p>
              </div>

              <ToggleRow
                title="Compact mode"
                description="Smaller spacing in lists and tables."
                on={compact}
                onChange={setCompact}
              />

              <div className="flex justify-end">
                <button className={primaryBtn} onClick={() => showToast("Appearance saved")}>
                  Save changes
                </button>
              </div>
            </div>
          )}

       
          {tab === "security" && (
            <div className="space-y-8">
              <div className="space-y-4">
                <h2 className="font-semibold">Change password</h2>
                <Field label="Current password" error={pwErrors.current}>
                  <input
                    type="password"
                    className={inputCls}
                    value={pw.current}
                    onChange={(e) => setPw({ ...pw, current: e.target.value })}
                  />
                </Field>
                <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
                  <Field label="New password" error={pwErrors.next}>
                    <input
                      type="password"
                      className={inputCls}
                      value={pw.next}
                      onChange={(e) => setPw({ ...pw, next: e.target.value })}
                    />
                  </Field>
                  <Field label="Confirm new password" error={pwErrors.confirm}>
                    <input
                      type="password"
                      className={inputCls}
                      value={pw.confirm}
                      onChange={(e) => setPw({ ...pw, confirm: e.target.value })}
                    />
                  </Field>
                </div>
                <div className="flex justify-end">
                  <button className={primaryBtn} onClick={savePassword}>
                    Update password
                  </button>
                </div>
              </div>

             
              <div className="rounded-xl border border-danger/30 bg-danger/10 p-5">
                <h2 className="font-semibold text-red-300">Delete account</h2>
                <p className="mt-1 text-sm text-muted">
                  This removes your account and cannot be undone.
                </p>

                {!confirmDelete ? (
                  <button
                    onClick={() => setConfirmDelete(true)}
                    className="mt-4 rounded-xl border border-danger/40 bg-danger/20 px-4 py-2 text-sm font-medium text-red-300 transition-all hover:bg-danger/30 active:scale-95"
                  >
                    Delete my account
                  </button>
                ) : (
                  <div className="mt-4 flex flex-col gap-3 sm:flex-row">
                    <input
                      className={inputCls}
                      placeholder='Type "DELETE" to confirm'
                      value={deleteText}
                      onChange={(e) => setDeleteText(e.target.value)}
                    />
                    <button
                      disabled={deleteText !== "DELETE"}
                      onClick={() => showToast("Account deletion needs the API (not connected yet)")}
                      className="shrink-0 rounded-xl border border-danger/40 bg-danger/40 px-4 py-2 text-sm font-medium text-white transition-all enabled:hover:bg-danger/60 enabled:active:scale-95 disabled:cursor-not-allowed disabled:opacity-40"
                    >
                      Confirm delete
                    </button>
                  </div>
                )}
              </div>
            </div>
          )}
        </section>
      </div>

      
      {toast && (
        <div className="fixed bottom-6 right-6 z-50 rounded-xl border border-white/20 bg-[#14141f]/85 px-4 py-3 text-sm shadow-2xl backdrop-blur-xl transition-all duration-300 starting:translate-y-3 starting:opacity-0">
          {toast}
        </div>
      )}
    </div>
  );
}