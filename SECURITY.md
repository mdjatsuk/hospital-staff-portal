# Security policy

## Scope

Hospital Staff Portal is a portfolio/demo project. Do not use it with real
patient information, production credentials, or an internet-facing clinical
workflow.

## Reporting a vulnerability

Please do not open a public issue for a suspected vulnerability. Contact the
repository owner privately through the contact options on the
[GitHub profile](https://github.com/mdjatsuk) and include:

- a description of the issue and its impact;
- reproducible steps or a minimal proof of concept; and
- any suggested mitigation.

Do not include credentials or real personal/health information in a report.

## Configuration

Secrets must be supplied through .NET User Secrets, environment variables, or
a deployment secret store. Never commit API keys, OAuth secrets, connection
string passwords, publish profiles, or real personal data.
