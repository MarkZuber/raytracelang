# must run from elevated command prompt
# assumes chocolatey is already installed

& cinst python3
& python ./get-pip.py
& pip install pylint
& pip install click colorama