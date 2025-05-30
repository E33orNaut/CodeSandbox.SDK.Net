// Just some notes for workflow and testing.

import { CodeSandbox } from "@codesandbox/sdk";
  
const sdk = new CodeSandbox();

const sandbox = await sdk.sandbox.create();
const result = await sandbox.shells.python.run("print(1+1)");

// Live clone the sandbox to a second version
const sandbox2 = await sandbox.fork();

// Checkpoint the sandbox by hibernating it
await sandbox.hibernate();