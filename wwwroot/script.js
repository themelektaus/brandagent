
function renderCircle($canvas, v, lineWidth, color)
{
    const ctx = $canvas.getContext('2d')
    ctx.clearRect(0, 0, $canvas.width, $canvas.height)
    
    const r = $canvas.width / 2
    const l = lineWidth / 2
    const p = Math.PI * 2

    ctx.beginPath()
    ctx.arc(r, r, r - l, 0, p)
    ctx.lineWidth = lineWidth
    ctx.strokeStyle = `${color}4`
    ctx.stroke()

    ctx.beginPath()
    ctx.arc(r, r, r - l, p * (1 - v), p)
    ctx.lineWidth = lineWidth / 2
    ctx.strokeStyle = `${color}f`
    ctx.stroke()
}
