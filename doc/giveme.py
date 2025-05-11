from pathlib import Path


# <mxCell id="loLQ8K4vY5dgnIhMLRgw-197" value="&lt;font style=&quot;font-size: 14px;&quot;&gt;OrganizerEventService&lt;/font&gt;" style="rounded=0;whiteSpace=wrap;html=1;" vertex="1" parent="1">

# s = 'swimlane;'
# content = Path("bullshit.drawio").read_bytes().decode().split("\n")
# # print(content)
# f = list(filter(lambda x: "<mxCell " in x and "value" in x and s in x, content))
# # f = filter("")Path("bullshit.drawio").read_bytes().decode().split("\n")
# # print(next(f))

# for i in f:
#     i = i[i.index("value"):]
#     i = i[i.index('"')+1:]
#     i = i[:i.index('"')]
#     print(i)


s = ''' User
Studio
Game
Genre
Tag
Comment
Topic
ChatFeedback
Message
Job
UserSubscription
UserReaction
UserComplains
ComplainTicket
Banned
Event
GameLibrary
&lt;div&gt;EventOrganizer&lt;/div&gt;
User
StatisticGame'''

es = [i.strip() for i in s.split('\n')]

for i, e in enumerate(es):
    start = 1
    posy = 40
    s = f'''        <mxCell id="loLQ8K4vY5dgnIhMLRgw_-%d" value="%s" style="swimlane;fontStyle=1;align=center;verticalAlign=top;childLayout=stackLayout;horizontal=1;startSize=30;horizontalStack=0;resizeParent=1;resizeParentMax=0;resizeLast=0;collapsible=1;marginBottom=0;whiteSpace=wrap;html=1;fillColor=#ffffff;strokeColor=#36393d;" parent="1" vertex="1">
          <mxGeometry x="%d" y="{-1160 + i * 40}" width="160" height="30" as="geometry">
            <mxRectangle x="%d" y="40" width="70" height="40" as="alternateBounds" />
          </mxGeometry>
        </mxCell>
    '''
    print(s % (start + i, e, posy, posy))
    posy += 40
