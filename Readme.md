# การ scaffold
```
dotnet ef dbcontext scaffold 'Server=localhost;database=mypos;user id=sa;password=Meng1234!; Database=mypos;' Microsoft.EntityFrameworkCore.SqlServer -o Models -c DatabaseContext --context-dir Database
```

# สถานะ
1. [สถานะ HTTP ] (https://developer.mozilla.org/en-US/docs/Web/HTTP/Status)
2. [Methods HTTP] (https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods)

# การ Post หลักงาก post เสร็จแล้ว และอยากได้ข้อมูลกลับมา ต้องทำตามนี้
1. กำหนด ใช้ Name 
``` [HttpGet("{product_id}", Name = "GetProduct")] ```  

```
       [HttpGet("{product_id}", Name = "GetProduct")]
        public IActionResult Get(int product_id)
        {
            try
            {
                Products data = Context.Products.SingleOrDefault(p => p.ProductId == product_id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception)
            {
                _logger.LogError("Failed to execute GET");
                return BadRequest();
            }
        }
```

2. ให้ Retrun ตรง POST  สังเกต ตรง 
```
return CreatedAtRoute("GetProduct", new { product_id = model.ProductId},model);  
```

```
      [HttpPost]
        public IActionResult Post([FromBody] Products model)
        {
            try
            {
                Context.Add(model);
                Context.SaveChanges();
                //return Created("GetProduct", new { product_id = model.ProductId});  //201 Insert แล้ว ให้ส่งข้อมูลกลับไปด้วย
                return CreatedAtRoute("GetProduct", new { product_id = model.ProductId},model);  //ส่งข้อมูลทั้งก่อน กลับไป 
            }
            catch (Exception)
            {
                _logger.LogError("Failed to execute POST");
                return BadRequest();
            }
        }
```

# CROS
1. [Html Test] (https://www.w3schools.com/html/tryit.asp?filename=tryhtml_intro)
2. Code สำหรับ Test
```
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Page Title</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
</head>
<body>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script>
        $.ajax({
            type: "GET",
            url: "https://localhost:5001/api/product",
            success: function (response) {
                console.log(response)
                alert(JSON.stringify(response))
            }
        });
    </script>
</body>
</html>
```

3. แก้ไขฝั่ง Web app ให้ปลดล็อค  สามารถ ปลดล็อคทั้งหมด หรือ เลือกบาง Domain 
4. กำหนดค่า ตรงไฟล์ Startup.cs ตรง  ConfigureServices

```
  services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });

                options.AddPolicy("AllowSpecific", builder =>
                {
                    builder.WithOrigins("http://localhost:4848", "http://www.codemobiles.com")
                           .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });

                options.AddPolicy("AllowSpecificMethods", builder =>
                {
                    builder.WithOrigins("http://localhost:4848", "http://www.codemobiles.com")
                           .WithMethods("GET", "POST", "HEAD", "PUT")
                           .AllowAnyHeader().AllowCredentials();
                });
            });
```

5. กำหนดที่  Configure ที่ไฟล์  Startup.cs 
```
    app.UseCors("AllowAll");
```

# Docker
1.
