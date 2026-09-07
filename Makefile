feature-branch:
	git switch --create $(BRANCH)

pr-from-main:
	test "$$(git default-branch)" == "$$(git branch --show-current)" || throw "Current branch is NOT default ($$(git branch --show-current)). Full PR cycle starts from forking the default branch."
	git switch --create $(BRANCH)
	git save "$(TITLE)"
	gh pr create --title "$(TITLE)" --body "" || true
	gh pr view --web

pr:
	test "$$(git default-branch)" != "$$(git branch --show-current)" || throw "Current branch is default ($$(git branch --show-current)). This is likely a mistake, PRs should be created from a feature branch."
	git save "$(TITLE)"
	gh pr create --title "$(TITLE)" --body "" || true
	gh pr view --web

post-pr:
	git default-and-burn
	git pull