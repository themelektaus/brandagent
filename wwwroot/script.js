
function renderCircle($canvas, v, lineWidth)
{
    console.log($canvas, v, lineWidth)

    const ctx = $canvas.getContext('2d')
    ctx.clearRect(0, 0, $canvas.width, $canvas.height)
    
    const r = $canvas.width / 2
    const l = lineWidth / 2
    const p = Math.PI * 2

    ctx.beginPath()
    ctx.arc(r, r, r - l, 0, p)
    ctx.lineWidth = lineWidth
    ctx.strokeStyle = "#99c4"
    ctx.stroke()

    ctx.beginPath()
    ctx.arc(r, r, r - l, p * (1 - v), p)
    ctx.lineWidth = lineWidth / 4
    ctx.strokeStyle = "#9cf"
    ctx.stroke()
}
